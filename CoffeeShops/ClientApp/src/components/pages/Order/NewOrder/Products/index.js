import React, { Component } from 'react'
import ProductList from '../../../Shop/Products/List'
import { Container } from './styled'

class OrderProducts extends Component {

    render() {
        return (
            <Container>
                <h2>Выберите продукты</h2>

                <ProductList client />
            </Container>
        )
    }
}

export default OrderProducts;