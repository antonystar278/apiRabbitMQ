import { authenticationService } from '../services/AuthenticationService';

export function authHeader() {
    
    const currentUser = authenticationService.currentUserValue;

    if (currentUser && currentUser.data.token) {  
        return { Authorization: currentUser.data.token };
    } else {
        return {};
    }
}