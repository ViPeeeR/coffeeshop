import axios from 'axios';
import history from './history';

export const setAuthToken = token => {
    if (token) {
        axios.defaults.headers.common['Authorization'] = "Bearer " + token;
    }
    else {
        delete axios.defaults.headers.common['Authorization'];
    }
}

export const handleResponse = () => {
    axios.interceptors.response.use(function (response) {
        // Do something with response data
        return response;
    }, function (error) {
        if (error.response.status === 401) {
            localStorage.removeItem('cs-token-access');
            setAuthToken(false);
            history.push('/');
        }
        // Do something with response error
        return Promise.reject(error);
    });
}