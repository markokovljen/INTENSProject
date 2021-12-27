import axios from "axios";

export default axios.create({
    baseURL: 'http://localhost:24974/api'
});