import React, { Component } from 'react'
import axios from 'axios'
import { Link } from 'react-router-dom'
import { Container, ControlPanel, Block } from './styled'
import { apiLoadShops, apiDeleteShop } from '../../../../api';

class ShopList extends Component {

    state = {
        shops: null
    }

    async componentWillMount() {
        await this.loadData();
    }

    loadData = async () => {
        let data = await apiLoadShops();
        this.setState({ shops: data });
        console.log(data);
    }

    onSelect = (event, id) => {
        event.preventDefault();
        this.props.onSelect(id);
    }

    deleteShop = async (event, id) => {
        event.preventDefault();
        await apiDeleteShop(id);
        await this.loadData();
    }

    render() {

        const { shops } = this.state
        const { admin, client } = this.props

        let shopsData = shops && shops.map((value, index) => {
            return (
                <div key={value.id}>
                    {
                        client
                            ?
                            <h3><a href="2" onClick={(event) => this.onSelect(event, value.id)}>{index + 1}. Магазин {value.id}</a></h3>
                            :
                            <h3>{index + 1}. Магазин {value.id}</h3>
                    }
                    <div>
                        <span>Название: {value.name}</span>
                    </div>
                    <div>
                        <span>Адрес: {value.address}</span>
                    </div>
                    <div>
                        <span>Номер телефона: {value.phone}</span>
                    </div>
                    <div>
                        <span>Количество товара: {value.products.length}</span>
                    </div>

                    {
                        admin ?
                            <ControlPanel>
                                <Block><Link to={`/shops/edit/${value.id}`}>Редактировать</Link></Block>
                                <Block><Link to={`/shops/products/list/${value.id}`}>Список товаров</Link></Block>
                                <Block><Link to={`/shops/products/create/${value.id}`}>Добавить товар</Link></Block>
                                <Block><a href="" onClick={(event) => this.deleteShop(event, value.id)}>Удалить</a></Block>
                            </ControlPanel>
                            : ''
                    }

                </div>
            )
        })

        return (
            <Container>
                {shopsData}
            </Container>
        );
    }
}

export default ShopList;