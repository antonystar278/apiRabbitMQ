import { BehaviorSubject } from 'rxjs';
import Axios from "axios";
import { async } from 'rxjs/internal/scheduler/async';

const currentUserSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('currentUser')));

const axios = Axios.create({
    baseURL: "https://localhost:5001/api/user",
    responseType: "json"
})

export const authenticationService = {
    login,
    logout,
    currentUser: currentUserSubject.asObservable(),
    get currentUserValue () { return currentUserSubject.value }
};

async function login(username, password) {

    const user = await axios.post('/authenticate', { username, password })

    localStorage.setItem('currentUser', JSON.stringify(user));
    currentUserSubject.next(user);

    return user;

}

function logout() {

    localStorage.removeItem('currentUser');
    currentUserSubject.next(null);
}