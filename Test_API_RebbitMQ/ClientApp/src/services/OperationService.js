import Axios from "axios";
import { authHeader } from '../helpers/AuthHeader';

export const operationService = {
    createOperation,
    getPaginatedOperations
};

const axios = Axios.create({
    baseURL: "https://localhost:5001/api/operation",
    responseType: "json"
})

async function createOperation(name, userId) {
    const auth = authHeader();
    const config = {
        headers: auth
    };
    return await axios.post('', { name, userId }, config);
}

async function getPaginatedOperations(page, size) {
    const auth = authHeader();
    const config = {
        headers: auth
    };

    return await axios.get(`?pageSize=${size}&pageIndex=${page}`, config);
} 