import React, { useState } from "react"
import { useHistory } from "react-router-dom"
import axios from "axios"
import {connect} from 'react-redux'
import { login } from "../actionCreators/userActionCreators"
import {isEmpty} from '../Helpers/HelpFunctions'

const EditUser = (props) => {
  const URL = "https://localhost:5001/api/users"
  const AuthStr = (!isEmpty(props.user)) ? `Bearer ${props.user.token}` : ""
  const isLoggedIn = props.isLogin
  const history = useHistory()

  const [userParams, setUserParams] = useState({    
        firstName: !isEmpty(props.user) ? props.user.firstName : "",
        lastName: !isEmpty(props.user) ? props.user.lastName : "",
        email: !isEmpty(props.user) ? props.user.email : "",
        photo:null,
        birthday: !isEmpty(props.user) ? props.user.birthday : "",
        phone: !isEmpty(props.user) ? props.user.phone : "",
        login: !isEmpty(props.user) ? props.user.login : "",
        password:""
      }) 

  const { firstName, lastName, email, photo, birthday, phone, login, password } = userParams
  const [confirmPassword, setConfirmPassword] = useState("")
  const [errorText, setErrorText] = useState("")

  const onInputChange = e => {
    setUserParams({ ...userParams, [e.target.name]: e.target.value })
  };

  const onInputPasswordChange = e => {
    setConfirmPassword(e.target.value)
  };

  const onSubmit = async e => {
    e.preventDefault()

    if(password == confirmPassword && password != ""){
        axios({
            method: 'put',
            url: `${URL}/${props.user.userId}`,
            data: userParams,
            headers: {'Authorization': `${AuthStr}`, 'Content-Type': 'application/json' }
            })
            .then(function (response) {
                console.log(userParams);
                var updatedUser = {
                    userId: props.user.userId,
                    firstName: userParams.firstName,
                    lastName: userParams.lastName,
                    email: userParams.email,
                    photo:null,
                    birthday: userParams.birthday,
                    phone: userParams.phone,
                    login: userParams.login,
                    password: userParams.password,
                    token: props.user.token
                }
                props.login(updatedUser)
                history.push("/tasks");
                console.log(response.data);
            })
            .catch(function (response) {
                console.log(response);
                setErrorText("Can not change this user")
            })
    } 
    else{
        setErrorText("Error in password")
    }           
  }
  
  if (isLoggedIn) {        
    return (
    <div className="container w-50 py-3 elem_center_50 settings_component">      
        <form onSubmit={e => onSubmit(e)}>

            <h1 className="text-center py-3">Changing user information:</h1>

            <div className="elem elem-center">
            
                <div className="px-3">
                    <div className="form-group">
                        <input 
                                type="text"
                                className="form-control form-control-lg"
                                placeholder="Enter firstname"
                                name="firstName"
                                value={firstName}
                                onChange={e => onInputChange(e)}
                        />
                    </div>  

                    <div className="form-group elem-between">
                        <input
                                    type="text"
                                    className="form-control form-control-lg"
                                    placeholder="Enter lastname"
                                    name="lastName"
                                    value={lastName}
                                    onChange={e => onInputChange(e)}
                            />
                    </div>

                    <div className="form-group">
                        <input 
                                type="email"
                                className="form-control form-control-lg"
                                placeholder="Enter email"
                                name="email"
                                value={email}
                                onChange={e => onInputChange(e)}
                        />
                    </div> 

                    <div className="form-group">
                        <input 
                                type="text"
                                className="form-control form-control-lg"
                                placeholder="Enter birthday"
                                name="birthday"
                                value={(birthday).substring(0, 10)}
                                onChange={e => onInputChange(e)}
                        />
                    </div> 

                </div> 

                <div className="px-3">

                    <div className="form-group">
                        <input 
                                type="text"
                                className="form-control form-control-lg"
                                placeholder="Enter phone"
                                name="phone"
                                value={phone}
                                onChange={e => onInputChange(e)}
                        />
                    </div> 

                    <div className="form-group">
                        <input 
                                type="text"
                                className="form-control form-control-lg"
                                placeholder="Enter login"
                                name="login"
                                value={login}
                                onChange={e => onInputChange(e)}
                        />
                    </div> 

                    <div className="form-group">
                        <input 
                                type="text"
                                className="form-control form-control-lg"
                                placeholder="Enter password"
                                name="password"
                                value={password}
                                onChange={e => onInputChange(e)}
                        />
                    </div> 

                    <div className="form-group">
                        <input 
                                type="text"
                                className="form-control form-control-lg"
                                placeholder="Confirm Password"
                                name="confirmPassword"
                                value={confirmPassword}
                                onChange={e => onInputPasswordChange(e)}
                        />
                    </div>
                </div>           
            </div> 

            <div style={{color: 'red'}}>
                {errorText}
            </div>            
            
            <div className="py-3 mx-5" >
                <input className="w-100" type='submit' value="Save changes"/>                  
            </div>

        </form>      
    </div>
  )}
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
        login: (val) => dispatch(login(val))
    }
}
  
export default connect(mapStateToProps,mapDispatchToProps)(EditUser);
