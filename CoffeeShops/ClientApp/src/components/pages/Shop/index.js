import React, { Component } from 'react'
import { Link } from 'react-router-dom'
import { Container } from './styled';
import ShopList from './List'


class Shop extends Component {

    render() {

        return (
            <Container>
                <span>Просмотр магазинов</span>
                <div>
                    <Link to="/">На главную</Link>
                </div>
                <ShopList admin />
            </Container>

        );
    }
}

export default Shop;