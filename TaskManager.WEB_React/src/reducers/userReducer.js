import { LOGIN, LOGOUT } from "../actions/userActions"

function userReducer(state, action) {
    switch (action.type) {
        case LOGIN:
            return {...state, isLogin: true, user: action.updateUser}
        case LOGOUT:
            return {isLogin: false}        
        default:
            return state
    }
}

export default userReducer