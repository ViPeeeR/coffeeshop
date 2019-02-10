import React, { Component } from 'react'
import ShopList from '../../../Shop/List'
import { Container } from './styled'

class Shop extends Component {

    componentWillMount() {
        console.log("я жив")
    }

    render() {
        return (
            <Container>
                <h1>Выберите магазин</h1>

                <ShopList client />
            </Container>
        )
    }
}

export default Shop;