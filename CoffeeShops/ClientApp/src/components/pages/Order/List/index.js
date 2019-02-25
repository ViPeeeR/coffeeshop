import React, { Component } from 'react'
import axios from 'axios'

import { Container } from './styled'
import { apiLoadOrders } from '../../../../api';

class OrderList extends Component {

    state = {
        orders: null
    }

    async componentWillMount() {
        let data = await apiLoadOrders();
        console.log(data);

        this.setState({ orders: data });
    }

    render() {

        const { orders } = this.state

        let orderData = orders && orders.map((value, index) => {
            const { shop, client } = value;
            return (
                <div key={value.id}>
                    <h3>{index + 1}. Заказ {value.id}</h3>
                    <div>
                        <span>Клиент: {client && client.lastName} {client && client.firstName} {client &&  client.middleName}</span>
                    </div>
                    <div>
                        <span>Магазин: {shop && shop.name}</span>
                    </div>
                    <div>
                        <span>Дата заказа: {new Date(value.date).toLocaleDateString()}</span>
                    </div>
                    <div>
                        <span>Дата доставки: {new Date(value.dateDelivery).toLocaleDateString()}</span>
                    </div>
                    <div>
                        <span>Статус: {value.statusOrder == 0 ? 'Готовится' : 'Курьер в пути'}</span>
                    </div>
                </div>
            )
        })

        return (
            <Container>
                {orderData}
            </Container>
        )
    }
}

export default OrderList;