import Axios from "axios";
import { OperationClient } from '../services/clients/OperationClient';
import { environment } from '../environments/environment';

export class OperationService {
    constructor() {
        this.axios = Axios.create({
            baseURL: environment.baseURL + "/operations",
            responseType: "json"
        });
        this.operationClient = new OperationClient();
    }

    async createOperation(name, userId) {
        const config = this.operationClient.getConfig();
        return await this.axios.post('', { name, userId }, config);
    }

    async getFilteredOperations(page, size) {
        const config = this.operationClient.getConfig();
        const queryString = this.operationClient.getQueryString(page, size);
    
        return await this.axios.get(queryString, config);
    }
}



 