import React, { Component } from 'react'
import { Link } from 'react-router-dom'
import OrderList from './List'
import { Container } from './styled'

class Order extends Component {

    render() {
        return (
            <Container>
                <span>Просмотр заказов</span>
                <div>
                    <Link to="/">На главную</Link>
                </div>
                <OrderList />
            </Container>
        )
    }
}

export default Order;