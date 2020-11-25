import React from "react";
import { Link, NavLink } from "react-router-dom";
import { connect } from "react-redux"
import { logout } from "../actionCreators/userActionCreators"

const Navbar = (props) => {

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
            <li className="nav-item">
              <NavLink className="nav-link" exact to="/tasks">
                Tasks
              </NavLink>
            </li>
            <li className="nav-item">
              <NavLink className="nav-link" exact to="/roles">
                Roles
              </NavLink>
            </li>
          </ul>
        </div>

        <div style={{display: !props.isLogin ? 'block' : 'none' }}>
            <Link className="btn btn-outline-light" to="/login">Log In</Link>
        </div>

        <div style={{display: props.isLogin ? 'block' : 'none' }}>
            <button className="btn btn-outline-light" onClick={() => props.logout()}>Log out</button>
        </div>
        
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