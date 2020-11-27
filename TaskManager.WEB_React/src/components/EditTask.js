import React, { useState, useEffect } from "react"
import axios from "axios"
import { useHistory, useParams } from "react-router-dom"
import {connect} from 'react-redux'
import {resetAppTasks} from "../actionCreators/userActionCreators"

const EditTask = (props) => {
  const URL = "https://localhost:5001/api/tasks"
  const AuthStr = (props.user !== undefined) ? `Bearer ${props.user.token}` : ""

  const { id } = useParams()
  const history = useHistory()
  const [task, setTask] = useState({
    taskId: "",
    userId: props.user.userId,
    taskTitle: "",
    taskDetails: "",
    taskCreationDate: "",
    taskStatus: false,
    taskPriority: "Low"    
  })

  const { taskId, taskTitle, taskDetails, taskCreationDate, taskStatus, taskPriority } = task

  const onInputChange = e => {
    setTask({ ...task, [e.target.name]: e.target.value })
  };

  const onSelectChange1= e => {
    setTask({ ...task, taskStatus: e.target.value })
  };

  const onSelectChange2= e => {
    setTask({ ...task, taskPriority: e.target.value })
  };

  useEffect(() => { loadTask() }, [])

  const onSubmit = async e => {
    e.preventDefault()
    if(id != 0){

      axios({
        method: 'put',
        url: `${URL}/${id}`,
        data: task,
        headers: {'Authorization': `${AuthStr}`, 'Content-Type': 'application/json' }
        })
        .then(function (response) {
          history.push("/tasks");
            console.log(response);
        })
        .catch(function (response) {
            console.log(response);
        })
    }
    else{
      axios({
        method: 'post',
        url: `${URL}`,
        data: task,
        headers: {'Authorization': `${AuthStr}`, 'Content-Type': 'application/json' }
        })
        .then(function (response) {
          history.push("/tasks");
            console.log(response);
        })
        .catch(function (response) {
            console.log(response);
        })
    }
    props.resetAppTasks()
  }

  const loadTask = async () => {
    if(id != 0){ 

        axios({
            method: 'get',
            url: `${URL}`,
            headers: {'Authorization': `${AuthStr}`, 'Content-Type': 'application/json' }
            })
            .then(function (response) {

                const result = response.data.find(x => x.taskId == id)
                setTask({
                    taskId: result.taskId,
                    userId: result.userId,
                    taskTitle: result.taskTitle,
                    taskDetails: result.taskDetails,
                    taskCreationDate: result.taskCreationDate,
                    taskStatus: result.taskStatus,
                    taskPriority: result.taskPriority
                  })
                console.log(response.data);
            })
            .catch(function (response) {
                console.log(response);
            });  
    }
    else{
      console.log("New task")
    }
  };
  
  return (
    <div className="container">
      <div className="w-75 mx-auto shadow p-5">
        <h2 className="text-center mb-4">Task information:</h2>
        <form onSubmit={e => onSubmit(e)}>
          <div className="form-group"> 
                taskId: {taskId}
          </div>
          <div className="form-group">
            <input
              type="text"
              className="form-control form-control-lg"
              placeholder="Enter taskTitle"
              name="taskTitle"
              value={taskTitle}
              onChange={e => onInputChange(e)}
            />
          </div>
          <div className="form-group">
            <input
              type="text"
              className="form-control form-control-lg"
              placeholder="Enter taskDetails"
              name="taskDetails"
              value={taskDetails}
              onChange={e => onInputChange(e)}
            />
          </div>
          <div className="form-group">
            <input
              type="text"
              className="form-control form-control-lg"
              placeholder="Enter taskCreationDate"
              name="taskCreationDate"
              value={(taskCreationDate).substring(0, 10)}
              onChange={e => onInputChange(e)}
            />
          </div>

          <div className="form-group">
            <select className="w-100 py-2" value={taskStatus} onChange={e => onSelectChange1(e)}>
              
                <option value="true">Completed</option>
                <option value="false">Running</option>
              
            </select>
          </div>

          <div className="form-group">
            <select className="w-100 py-2" value={taskPriority} onChange={e => onSelectChange2(e)}>
              
                <option value="Low">Low</option>
                <option value="Medium">Medium</option>
                <option value="High">High</option>
              
            </select>
          </div>

          <button className="btn btn-warning btn-block">Accept</button>
        </form>
      </div>
    </div>
  )
}

function mapStateToProps(state) {
    return state
}

function mapDispatchToProps(dispatch) {
  return {
      resetAppTasks: () => dispatch(resetAppTasks())
  }
}

export default connect(mapStateToProps,mapDispatchToProps)(EditTask);
