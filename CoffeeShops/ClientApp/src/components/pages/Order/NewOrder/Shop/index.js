import React, { Component } from 'react'
import { Redirect } from 'react-router'
import ShopList from '../../../Shop/List'
import { Container } from './styled'

class Shop extends Component {

    state = {
        toProducts: false
    }

    onSelect = (id) => {
        this.props.onSelect(id);
        this.setState({ toProducts: true })
    }

    render() {
        const { toProducts } = this.state

        return (
            <Container>
                {
                    toProducts ? <Redirect to="/orders/create/products" /> : ''
                }

                <h1>Выберите магазин</h1>

                <ShopList client onSelect={this.onSelect} />
            </Container>
        )
    }
}

export default Shop;