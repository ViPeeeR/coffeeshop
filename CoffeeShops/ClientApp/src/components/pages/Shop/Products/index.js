import React, { Component } from 'react'
import { Link } from 'react-router-dom'
import ProductList from './List'

import { Container, ControlPanel, Block } from './styled'

class Products extends Component {

    render() {

        return (
            <Container>
                <span>Просмотр товаров</span>
                <ControlPanel>
                    <Block><Link to="/">На главную</Link></Block>
                    <Block><Link to={`/shops`}>Список магазинов</Link></Block>
                    <Block><Link to={`/shops/products/create/${this.props.match.params.shopId}`}>Добавить товар</Link></Block>
                </ControlPanel>
                <ProductList admin />
            </Container>

        );
    }
}

export default Products;