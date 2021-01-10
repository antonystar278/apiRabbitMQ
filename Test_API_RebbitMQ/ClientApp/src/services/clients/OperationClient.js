import { authHeader } from '../../helpers/AuthHeader';

export class OperationClient {
    constructor() {
        this.auth = authHeader();
        this.configuration = {
            headers: this.auth
        };
      }

    getConfig() {
        return this.configuration;
    }

    getQueryString(page, size) { 
        const queryString = `?pageSize=${size}&pageIndex=${page-1}`;      
        return queryString;
    }

}