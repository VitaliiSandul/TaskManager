import React, {useState} from "react"
import axios from "axios"
import {connect} from 'react-redux'
import { useHistory } from "react-router-dom"

const Register = (props) => {
  const URL = "https://localhost:5001/api/users"
  const history = useHistory()
  
  const [userParams, setUserParams] = useState({
        firstName:"",
        lastName:"",
        email:"",
        photo:null,
        birthday:"",
        phone:"",
        login:"",
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

    if(password == confirmPassword){
        axios({
            method: 'post',
            url: URL,
            data: userParams,
            headers: { 'Content-Type': 'application/json' }
            })
            .then(function (response) {
                history.push("/login");
                console.log(response.data);
            })
            .catch(function (response) {
                console.log(response);
                setErrorText("Can not register this user")
            })
    } 
    else{
        setErrorText("Error in password")
    }           
  }

  return (
    <div className="container">      
        <form onSubmit={e => onSubmit(e)}>
          <div>
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
                        value={birthday}
                        onChange={e => onInputChange(e)}
                />
            </div> 

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
                        type="password"
                        className="form-control form-control-lg"
                        placeholder="Enter password"
                        name="password"
                        value={password}
                        onChange={e => onInputChange(e)}
                />
            </div> 

            <div className="form-group">
                <input 
                        type="password"
                        className="form-control form-control-lg"
                        placeholder="Confirm Password"
                        name="confirmPassword"
                        value={confirmPassword}
                        onChange={e => onInputPasswordChange(e)}
                />
            </div> 

            <div style={{color: 'red'}}>
                {errorText}
            </div>
            
            
            <div className="py-3" >
                <input className="w-100" type='submit' value="Register"/>                  
            </div>
            
          </div>          
        </form>      
    </div>
  )
}

function mapStateToProps(state) {
    return state
}

export default connect(mapStateToProps)(Register);
