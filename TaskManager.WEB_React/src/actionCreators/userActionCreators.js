import {LOGIN, LOGOUT} from '../actions/userActions'

// export const login = () => ({type: LOGIN})
export const logout = () => ({type: LOGOUT})


export function login(value) {
    return ({
      type: LOGIN,
      updateUser: value
    });
  }