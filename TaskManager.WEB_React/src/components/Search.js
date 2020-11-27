import React, {useState} from "react"
import axios from "axios"
import {connect} from 'react-redux'
import {setAppTasks} from "../actionCreators/userActionCreators"


const Search = (props) => {
  const URL = "https://localhost:5001/api/tasks/search"
  const AuthStr = (props.user !== undefined) ? `Bearer ${props.user.token}` : ""
  const [searchParams, setSearchParams] = useState({
    taskTitle: "",
    taskDetails: ""   
  })

  const { taskTitle, taskDetails } = searchParams

  const onInputChange = e => {
    setSearchParams({ ...searchParams, [e.target.name]: e.target.value })
  };

  const onSubmit = async e => {
    e.preventDefault()

    axios({
        method: 'get',
        url: `${URL}?userid=${props.user.userId}&tasktitle=${searchParams.taskTitle}&taskdetails=${searchParams.taskDetails}`,
        headers: {'Authorization': `${AuthStr}`, 'Content-Type': 'application/json' }
        })
        .then(function (response) {
            props.setAppTasks(response.data)
            console.log(response.data);
        })
        .catch(function (response) {
            console.log(response);
        })    
  }

  return (
    <div className="container">      
        <form onSubmit={e => onSubmit(e)}>
          <div className="d-flex justify-content-end">
            <div className="form-group d-flex">

                <input 
                        type="text"
                        className="form-control form-control-lg"
                        placeholder="Enter taskTitle"
                        name="taskTitle"
                        value={taskTitle}
                        onChange={e => onInputChange(e)}
                />
            </div>  

            <div className="form-group elem-between">
                <input
                            type="text"
                            className="form-control form-control-lg"
                            placeholder="Enter taskDetails"
                            name="taskDetails"
                            value={taskDetails}
                            onChange={e => onInputChange(e)}
                    />
            </div>
            
            <button className="btn btn-primary form-group">Search</button>
            
          </div>          
        </form>      
    </div>
  )
}

function mapStateToProps(state) {
    return state
}

function mapDispatchToProps(dispatch) {
    return {
        setAppTasks: (val) => dispatch(setAppTasks(val))
    }
}

export default connect(mapStateToProps,mapDispatchToProps)(Search);
