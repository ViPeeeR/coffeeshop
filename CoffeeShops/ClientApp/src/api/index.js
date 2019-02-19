import axios from 'axios'

export const apiLoadClient = async (id) => await axios
    .get(`/api/v1/client/${id}`)
    .then(({ data }) => data)
    .catch(ex => { alert(ex) });

export const apiLoadShop = async (id) => await axios
    .get(`/api/v1/shop/${id}`)
    .then(({ data }) => data)
    .catch(ex => { alert(ex) });

export const apiLoadClients = async () => await axios
    .get('/api/v1/client')
    .then(({ data }) => data)
    .catch(ex => { alert(ex) });

export const apiLoadShops = async () => await axios
    .get('/api/v1/shop')
    .then(({ data }) => data)
    .catch(ex => { alert(ex) });

export const apiLoadOrders = async () => await axios
    .get('/api/v1/order')
    .then(({ data }) => data)
    .catch(ex => { alert(ex) });

export const apiLoadProducts = async (id) => await axios
    .get(`/api/v1/product?shopId=${id}`)
    .then(({ data }) => data)
    .catch(ex => { alert(ex) });

export const apiLoadProduct = async (id) => await axios
    .get(`/api/v1/product/${id}`)
    .then(({ data }) => data)
    .catch(ex => { alert(ex) });

export const apiCreateClient = async (values) => await axios
    .post('/api/v1/client', values)
    .catch(ex => { alert(ex); return ex; });

export const apiUpdateClient = async (values) => await axios
    .put('/api/v1/client', values)
    .catch(ex => { alert(ex); return ex; });

export const apiCreateOrder = async (values) => await axios
    .post('/api/v1/order', values)
    .catch(ex => { alert(ex); return ex; });

export const apiCreateShop = async (values) => await axios
    .post('/api/v1/shop', values)
    .catch(ex => { alert(ex); return ex; });

export const apiUpdateShop = async (values) => await axios
    .put('/api/v1/shop', values)
    .catch(ex => { alert(ex); return ex; });

export const apiCreateProduct = async (values) => await axios
    .post('/api/v1/product', values)
    .catch(ex => { alert(ex); return ex; });

export const apiUpdateProduct = async (values) => await axios
    .put('/api/v1/product', values)
    .catch(ex => { alert(ex); return ex; });

export const apiDeleteProduct = async (id) => await axios
    .delete(`/api/v1/product/${id}`)
    .catch(ex => { alert(ex); return ex; });

export const apiDeleteClient = async (id) => await axios
    .delete(`/api/v1/client/${id}`)
    .catch(ex => { alert(ex); return ex; });

export const apiDeleteShop = async (id) => await axios
    .delete(`/api/v1/shop/${id}`)
    .catch(ex => { alert(ex); return ex; });

// TODO: запрос access_token и refresh_token