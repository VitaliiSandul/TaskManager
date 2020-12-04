import React, { useEffect, useState } from 'react'
import {connect} from 'react-redux'
import axios from 'axios'
import {arrayContains, isEmpty} from '../Helpers/HelpFunctions'
import deleteimg from '../images/delete.png'

function Users(props) {
    const URL = `https://localhost:5001/api/users`    
    const AuthStr = (props.user !== undefined) ? `Bearer ${props.user.token}` : ""
    const isLoggedIn = props.isLogin    
    
    const [users, setUsers] = useState([])
    useEffect(() => {
                        loadUsers()
                        loadRoles()
                    }, [])

    const [roles, setRoles] = useState([])
    const [selectedRole, setSelectedRole] = useState("")

    const handleCheckboxChange = async (event, userId, roleId) => {
        const {name, value, checked, type} = event.target
        console.log(name + " " + value)
        console.log(checked)

        setSelectedRole({
            [name]: type === 'checkbox' ? checked : value            
        })  
        
        if(checked){
            await axios({
                        method: 'post',
                        url: "https://localhost:5001/api/userroles/setrole",
                        data: JSON.stringify({UserId: userId, RoleId: roleId}),
                        headers: {'Authorization': `${AuthStr}`, 'Content-Type': 'application/json' }
                        })
                        .then(function (response) {
                            console.log(response.data);
                        })
                        .catch(function (response) {
                            console.log(response);
                        })            
        }
        else{
            await axios({
                        method: 'delete',
                        url: `https://localhost:5001/api/userroles/deleterole/${userId}`,
                        data: JSON.stringify({RoleId: roleId}),
                        headers: {'Authorization': `${AuthStr}`, 'Content-Type': 'application/json' }
                        })
                        .then(function (response) {
                            console.log(response.data);
                        })
                        .catch(function (response) {
                            console.log(response);
                        })
        }
    }

    const loadRoles = async () => {
        await axios({
                    method: 'get',
                    url: `https://localhost:5001/api/roles`,
                    headers: {'Authorization': `${AuthStr}`, 'Content-Type': 'application/json' }
                    })
                    .then(function (response) {
                        setRoles(response.data)
                        console.log(response.data);
                    })
                    .catch(function (response) {
                        console.log(response);
                    }) 
    }

    const loadUsers = async () => {
        await axios({
                    method: 'get',
                    url: URL,
                    headers: {'Authorization': `${AuthStr}`, 'Content-Type': 'application/json' }
                    })
                    .then(function (response) {
                        setUsers(response.data)
                        console.log(response.data);
                    })
                    .catch(function (response) {
                        console.log(response);
                    })
    }

    const removeUser = (id) => {
        axios.delete(`${URL}/${id}`, { headers: { Authorization: AuthStr } })
        setUsers(users.filter(x => x.userId !== id));
    }
    
    const ifAdmin = (props.user !== undefined && !isEmpty(props.user)) ? arrayContains("Admin", props.user.role) : false
    
    if (isLoggedIn && ifAdmin ) {        
        return (
            <div>                
                <div className="margin_table">

                    <table className="table table table-striped table-bordered">
                        <thead className="thead-dark">
                            <tr>
                                <th className="text-center">User Id</th>
                                <th className="text-center">FirstName</th>
                                <th className="text-center">LastName</th>
                                <th className="text-center">Email</th>
                                <th className="text-center">Birthday</th>
                                <th className="text-center">Phone</th>
                                <th className="text-center">Login</th>
                                <th className="text-center">Roles</th>
                                <th className="text-center" colSpan="2">Operation</th>
                            </tr>
                        </thead>
                        <tbody>
                            {users.map((x, i)=> 
                            <tr key={i}>
                                <td className="text-center">{x.userId}</td>
                                <td className="text-center">{x.firstName}</td>
                                <td className="text-center">{x.lastName}</td>
                                <td className="text-center">{x.email}</td>
                                <td className="text-center">{(x.birthday).substring(0, 10)}</td>
                                <td className="text-center">{x.phone}</td>
                                <td className="text-center">{x.login}</td>
                                <td className="text-center">
                                    {roles.map((rl, j) =>
                                    <div key={j} className="elem-between w-auto">
                                        <div className="mx-1">
                                            {rl.roleName}
                                        </div>
                                        <div className="mx-1">
                                            <input type="checkbox" 
                                                    name="selectedRole" 
                                                    defaultChecked={arrayContains(rl.roleName, x.role) ? true : false} 
                                                    onChange={e => handleCheckboxChange(e, x.userId, rl.roleId)}
                                                    className="w-auto"/>
                                        </div>
                                    </div>)}
                                </td>
                                <td className="text-center">
                                    { 
                                        x.userId > 1 ?
                                        <div>
                                            <img src={deleteimg} onClick={() => removeUser(x.userId)} height="40" width="40" alt="Delete" title="Delete User" style={{ cursor: 'pointer' }}/>
                                        </div>
                                        :
                                        <div></div>
                                    }                                    
                                </td>

                            </tr>)}
                        </tbody>
                    </table> 

                </div>           
            </div>
        )
    }
    else{
        return (
            <h1 className="text-center py-5">You don't have access to this page, please log in.</h1>
        )
    }
}

function mapStateToProps(state) {
    return state
}
  
export default connect(mapStateToProps)(Users);