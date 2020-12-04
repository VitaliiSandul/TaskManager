import React, {Component} from 'react'
import axios from "axios"
import { connect } from "react-redux"
import { login } from "../actionCreators/userActionCreators"
import { Link, Redirect } from "react-router-dom";

class Login extends Component {
  constructor(props) {
    super(props)
    this.state = { 
                   username: "",
                   password: "",
                   errorText: ""
                 }
  }  
  
  handleChange = event => {
    this.setState({
      [event.target.name]: event.target.value
    });
  }

  handleSubmit = event => {
    event.preventDefault()
    
    axios({
      method: 'POST',
      url: 'https://localhost:5001/api/auth',
      headers: { 'Content-Type': 'application/json' },
      data: JSON.stringify({login: this.state.username, password: this.state.password})
    }).then((response) => {
                            this.props.login(response.data)
                            console.log(response.data)
                          }
    ).catch((response) => {
      this.setState({ errorText: "Wrong login or password." })              
      console.log(response)    
    });
  }

  render() {
    console.log(this.props.user)
    
    const isLoggedIn = this.props.isLogin;
    
    if (!isLoggedIn) {
      return (
        <div className="">
          <div className="elem_center_50 elem-center elem_border text-center login_component p-3">
              <form onSubmit={this.handleSubmit}>
    
                <h1>Please, log in</h1>
    
                <div className="elem-around">
                    <div className="py-1 mr-1">
                        <label>Username</label>
                    </div>
                  
                    <div className="py-1">
                        <input name='username'
                              placeholder='Username'
                              value={this.state.username}
                              onChange={this.handleChange}
                        />
                    </div>
                </div>
                
                <div className="elem-around">
                    <div className="py-1 mr-1">
                        <label>Password</label>
                    </div>
                  
                    <div className="py-1">
                        <input type='password'
                              name='password'
                              placeholder='Password'
                              value={this.state.password}
                              onChange={this.handleChange}
                          />
                    </div>
                </div>

                <div style={{color: 'red'}}>
                  {this.state.errorText}
                </div>
    
                <div className="py-3 text-right" >
                  <Link to="/register">Register new account</Link>                                  
                </div>

                <div className="py-3" >
                  <input className="w-100" type='submit' value="Log In"/>                  
                </div>

              </form>
    
          </div> 
        </div>       
      )

    } else {
      return(
        <Redirect to={"/tasks"} />
      )      
    }
    
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

export default connect(mapStateToProps,mapDispatchToProps)(Login)