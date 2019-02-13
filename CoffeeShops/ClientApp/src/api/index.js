import axios from 'axios'

export const apiLoadClient = async (id) => await axios
    .get(`/api/v1/client/${id}`)
    .then(({ data }) => data);

export const apiLoadShop = async (id) => await axios
    .get(`/api/v1/shop/${id}`)
    .then(({ data }) => data);

export const apiLoadClients = async () => await axios
    .get('/api/v1/client')
    .then(({ data }) => data);

export const apiLoadShops = async () => await axios
    .get('/api/v1/shop')
    .then(({ data }) => data);

export const apiLoadOrders = async () => await axios
    .get('/api/v1/order')
    .then(({ data }) => data);

export const apiLoadProducts = async (id) => await axios
    .get(`/api/v1/product?shopId=${id}`)
    .then(({ data }) => data);

export const apiLoadProduct = async (id) => await axios
    .get(`/api/v1/product/${id}`)
    .then(({ data }) => data);

export const apiCreateClient = async (values) => await axios.post('/api/v1/client', values);

export const apiUpdateClient = async (values) => await axios.put('/api/v1/client', values);

export const apiCreateOrder = async (values) => await axios.post('/api/v1/order', values);

export const apiCreateShop = async (values) => await axios.post('/api/v1/shop', values);

export const apiUpdateShop = async (values) => await axios.put('/api/v1/shop', values);

export const apiCreateProduct = async (values) => await axios.post('/api/v1/product', values);

export const apiUpdateProduct = async (values) => await axios.put('/api/v1/product', values);

export const apiDeleteProduct = async (id) => await axios.delete(`/api/v1/product/${id}`);