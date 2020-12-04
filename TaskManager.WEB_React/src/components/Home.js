import React from 'react'
import { Link } from "react-router-dom";
import 'bootstrap/dist/css/bootstrap.min.css'
import '../index.css'
import logo from '../images/task_mng.png'

function Home() {
  return (
    <div className="elem text-center">
           <h1>Task Manegment Software</h1>
           <span>To continue please <Link to="/login">login</Link>, if you don't have account <Link to="/register">register</Link> it</span>
           <div className="home-img">
              <img src={logo} className="elem"/>
           </div>
    </div>
  );
}

export default Home;