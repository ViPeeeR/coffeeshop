import React, { Component } from 'react'
import OrderList from './List'
import { Container } from './styled'

class Order extends Component {

    render() {
        return (
            <Container>
                <OrderList />
            </Container>
        )
    }
}

export default Order;