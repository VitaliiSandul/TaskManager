import { LOGIN, LOGOUT, SETAPPTASKS, RESETAPPTASKS } from "../actions/userActions"

function userReducer(state, action) {
    switch (action.type) {
        case LOGIN:
            return {...state, isLogin: true, user: action.updateUser}
        case LOGOUT:
            return {isLogin: false, apptasks: []}    
        case SETAPPTASKS:
            return {...state, apptasks: action.payload} 
        case RESETAPPTASKS:
            return {...state, apptasks: []}    
        default:
            return state
    }
}

export default userReducer