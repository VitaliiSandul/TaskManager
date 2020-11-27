import React, { useEffect} from 'react'
import {connect} from 'react-redux'
import {setAppTasks} from "../actionCreators/userActionCreators"
import {Link} from 'react-router-dom'
import Button from 'react-bootstrap/Button'
import axios from 'axios'
import Search from './Search' 

function Tasks(props) {
    const url = `https://localhost:5001/api/tasks`    
    const AuthStr = (props.user !== undefined) ? `Bearer ${props.user.token}` : ""
    const isLoggedIn = props.isLogin
    
    useEffect(() => loadTasks(), [])

    const loadTasks = async () => {
        console.log("props.apptasks.length");
        console.log(props.apptasks.length);

        if(props.apptasks.length === 0){
            await axios.get(`${url}/search?userid=${props.user.userId}`, 
                    { headers: { Authorization: AuthStr } })
                    .then(response => {
                        console.log(response.data);
                        props.setAppTasks(response.data);
                    })
                    .catch((error) => {
                        console.log('error ' + error);
                    })
        }    
    }

    const removeTask = (id) => {
        axios.delete(`${url}/${id}`, { headers: { Authorization: AuthStr } })
        props.setAppTasks(props.apptasks.filter(x => x.taskId !== id));
    }
    
    if (isLoggedIn) {        
        return (
            <div>                
                <div className="margin_table">
                    <div className="d-flex">
                        <Button variant="outline-success" className="w-25 form-group">
                            <Link to={{pathname: `edittask/0`}}>Add new task</Link>
                        </Button>

                        <Search className="w-75"/>
                    </div>
                    

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
                            {props.apptasks.map((x, i)=> 
                            <tr key={i}>
                                <td className="text-center">{x.taskId}</td>
                                <td className="text-center">{x.userId}</td>
                                <td>{x.taskTitle}</td>
                                <td>{x.taskDetails}</td>
                                <td className="text-center">{(x.taskCreationDate).substring(0, 10)}</td>
                                <td className="text-center" style={{color: x.taskStatus ? 'green' : 'red'}}>{(x.taskStatus) ? "Completed" : "Running"}</td>
                                <td className="text-center">{x.taskPriority}</td>
                                <td className="text-center">
                                    <Button variant="outline-danger" onClick={() => removeTask(x.taskId)}>
                                        Remove
                                    </Button>
                                </td>
                                <td className="text-center">
                                    <Button variant="outline-primary" >
                                        <Link to={{pathname: `edittask/${x.taskId}`}}>Edit</Link>
                                    </Button>
                                </td>
                            </tr>)}
                        </tbody>
                    </table> 
                    
                    

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

function mapDispatchToProps(dispatch) {
    return {
        setAppTasks: (val) => dispatch(setAppTasks(val))
    }
}
  
export default connect(mapStateToProps,mapDispatchToProps)(Tasks);