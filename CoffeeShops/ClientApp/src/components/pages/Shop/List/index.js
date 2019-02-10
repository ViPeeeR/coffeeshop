import React, { Component } from 'react'
import axios from 'axios'
import { Link } from 'react-router-dom'
import { Container, ControlPanel, Block } from './styled'

class ShopList extends Component {

    state = {
        shops: null
    }

    async componentWillMount() {
        let data = await axios.get('/api/v1/shop')
            .then(({ data }) => data);

        console.log(data);

        this.setState({ shops: data });
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
                            <h3><Link to={`/orders/create/products/${value.id}`}>{index + 1}. Магазин {value.id}</Link></h3>
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