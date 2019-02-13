import React, { Component } from 'react'
import axios from 'axios'

import { Container } from './styled'

class OrderList extends Component {

    state = {
        orders: null
    }

    async componentWillMount() {
        let data = await axios.get('/api/v1/order')
            .then(({ data }) => data);

        console.log(data);

        this.setState({ orders: data });
    }

    render() {

        const { orders } = this.state

        let orderData = orders && orders.map((value, index) => {
            return (
                <div key={value.id}>
                    <h3>{index + 1}. Заказ {value.id}</h3>
                    <div>
                        <span>Клиент: {value.client.lastName} {value.client.firstName} {value.client.middleName}</span>
                    </div>
                    <div>
                        <span>Магазин: {value.shop.name}</span>
                    </div>
                    <div>
                        <span>Дата заказа: {new Date(value.date).toLocaleDateString()}</span>
                    </div>
                    <div>
                        <span>Дата доставки: {new Date(value.dateDelivery).toLocaleDateString()}</span>
                    </div>
                    <div>
                        <span>Статус: {value.statusOrder}</span>
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