import React from 'react'
import ReactDOM from 'react-dom'
import './index.css'
import 'bootstrap/dist/css/bootstrap.min.css'
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

const initialState = {isLogin: false, user: {}}
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

    </Provider>
  </React.StrictMode>,
  document.getElementById('root')
);

