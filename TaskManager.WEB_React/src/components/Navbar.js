import React from "react";
import { Link, NavLink } from "react-router-dom";
import { connect } from "react-redux"
import { logout } from "../actionCreators/userActionCreators"
import {isEmpty} from '../Helpers/HelpFunctions'
import settingsimg from '../images/settings.png'

const Navbar = (props) => {
  
    var ifAdmin = (props.user !== undefined && Array.isArray(props.user.role)) ? props.user.role.indexOf("Admin") > -1 : false  
    var editUrl = (props.user !== undefined && !isEmpty(props.user)) ? `/edituser/${props.user.userId}` : ""
    
    console.log("editUrl")
    console.log(editUrl)
  return (      
    <nav className="navbar navbar-expand-lg navbar-dark bg-primary">
      <div className="container">
        <Link className="navbar-brand" href="/">
            { props.isLogin ? <div>{props.user.login}</div> : <div>Unknown</div> }
        </Link>
        <button
          className="navbar-toggler"
          type="button"
          data-toggle="collapse"
          data-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>

        <div className="collapse navbar-collapse" id="navbarSupportedContent">
          <ul className="navbar-nav mr-auto">
            <li className="nav-item">
              <NavLink className="nav-link" exact to="/">
                Home
              </NavLink>
            </li>

            { props.isLogin ? 
                              <li className="nav-item">
                                <NavLink className="nav-link" exact to="/tasks">
                                  Tasks
                                </NavLink>
                              </li> : <div></div>}

            { (props.isLogin && ifAdmin) ? 
                                            <li className="nav-item">
                                              <NavLink className="nav-link" exact to="/users">
                                                Users
                                              </NavLink>
                                            </li> : <div></div>}           

          </ul>
        </div>

        <div style={{display: !props.isLogin ? 'block' : 'none' }}>
            <Link className="btn btn-outline-light" to="/login">Log In</Link>
        </div>

        <div style={{display: props.isLogin ? 'block' : 'none' }}>
            <button className="btn btn-outline-light" onClick={() => props.logout()}>Log out</button>
        </div>
        
          {<Link className="navbar-brand" to={editUrl}>
              { props.isLogin ? <div><img src={settingsimg} height="40" width="40" className="ml-4" title="Settings"/></div> : <div></div> }
          </Link>}
        
      </div>
    </nav>
  );
};

function mapStateToProps(state) {
    return state
}

function mapDispatchToProps(dispatch) {
    return {
        logout: () => dispatch(logout())
    }
}

export default connect(mapStateToProps,mapDispatchToProps)(Navbar);