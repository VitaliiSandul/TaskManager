import React from 'react'
import ReactDOM from 'react-dom'
import './index.css'
// import 'jquery/dist/jquery'
// import 'bootstrap/dist/js/bootstrap'
import 'bootstrap/dist/css/bootstrap.min.css'
// import App from './components/App'
import Home from './components/Home'
import Login from './components/Login'
import NotFound from './components/NotFound'
import Navbar from './components/Navbar'
import {Provider} from 'react-redux'
import {createStore} from 'redux'
import userReducer from './reducers/userReducer'
import {
  BrowserRouter as Router,
  Route,
  Switch
} from "react-router-dom";

// const initialState = {count: 0, value: 0}
// let store = createStore(counterReducer, initialState)


const initialState = {isLogin: false, user: {userId: "", login: ""}}
let store = createStore(userReducer, initialState)

ReactDOM.render(
  <React.StrictMode>
    <Provider store={store}>

    <Router>
      <div className="App">
        <Navbar />

        <Switch>
          <Route exact path="/" component={Home} />
          <Route exact path="/login" component={Login} />
          <Route component={NotFound} />
        </Switch>
      </div>
    </Router>
     {/* <App /> */}

    </Provider>
  </React.StrictMode>,
  document.getElementById('root')
);

