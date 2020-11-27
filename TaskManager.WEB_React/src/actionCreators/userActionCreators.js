import {LOGIN, LOGOUT, SETAPPTASKS, RESETAPPTASKS} from '../actions/userActions'

export const logout = () => ({type: LOGOUT})

export function login(value) {
    return ({
      type: LOGIN,
      updateUser: value
    });
  }

  export function setAppTasks(value) {
    return ({
      type: SETAPPTASKS,
      payload: value
    });
  }

  export const resetAppTasks = () => ({type: RESETAPPTASKS})