import React, { useEffect, useState } from 'react'
import {connect} from 'react-redux'
import {Link} from 'react-router-dom'
import Button from 'react-bootstrap/Button'
import axios from 'axios'

function Tasks(props) {
    const url = `https://localhost:5001/api/tasks`    
    const AuthStr = (props.user !== undefined) ? `Bearer ${props.user.token}` : ""
    console.log(AuthStr);

    let [tasks, setTasks] = useState([])

    const removeTask = (id) => {
        axios.delete(`${url}/${id}`, { headers: { Authorization: AuthStr } })
        loadTasks()
    }

    useEffect(() => loadTasks(), [])

    const loadTasks = async () => {
        await axios.get(`${url}/search?userid=${props.user.userId}`, 
                    { headers: { Authorization: AuthStr } })
                    .then(response => {
                        console.log(response.data);
                        setTasks(response.data);
                    })
                    .catch((error) => {
                        console.log('error ' + error);
                    })
    }

    const isLoggedIn = props.isLogin
    
    if (isLoggedIn) {        
        return (
            <div>
                
                    <div className="margin_table">

                        <table className="table table table-striped table-bordered">
                            <thead className="thead-dark">
                                <tr>
                                    <th className="text-center">Task Id</th>
                                    <th className="text-center">User Id</th>
                                    <th className="text-center">Title</th>
                                    <th className="text-center">Details</th>
                                    <th className="text-center">Creation Date</th>
                                    <th className="text-center">Status</th>
                                    <th className="text-center">Priority</th>
                                    <th className="text-center" colSpan="2">Operation</th>
                                </tr>
                            </thead>
                            <tbody>
                                {tasks.map((x, i)=> 
                                <tr key={i}>
                                    <td>{x.taskId}</td>
                                    <td>{x.userId}</td>
                                    <td>{x.taskTitle}</td>
                                    <td>{x.taskDetails}</td>
                                    <td>{(x.taskCreationDate).substring(0, 10)}</td>
                                    <td style={{color: x.taskStatus ? 'green' : 'red'}}>{(x.taskStatus) ? "Completed" : "Running"}</td>
                                    <td>{x.taskPriority}</td>
                                    <td>
                                        <Button variant="outline-danger" onClick={() => removeTask(x.taskId)}>
                                            Remove
                                        </Button>
                                    </td>
                                    <td>
                                        <Button variant="outline-primary" >
                                            <Link to={{pathname: `edittask/${x.taskId}`}}>Edit</Link>
                                        </Button>
                                    </td>
                                </tr>)}
                            </tbody>
                        </table>  

                        
                        <Button variant="outline-success" className="w-100">
                            <Link to={{pathname: `edittask/0`}}>Add new task</Link>
                        </Button>

                    </div>           
            </div>
        )
    }
    else{
        return (
            <h1 className="text-center py-5">You don't have access to this page, please log in.</h1>
        )
    }

}

function mapStateToProps(state) {
    return state
}

export default connect(mapStateToProps)(Tasks);