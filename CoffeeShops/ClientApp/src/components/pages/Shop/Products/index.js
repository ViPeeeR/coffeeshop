import React, { Component } from 'react'
import axios from 'axios'
import { Link } from 'react-router-dom'
import { Container } from './styled'
import { ControlPanel, Block } from './styled';


class Products extends Component {

    state = {
        products: null
    }

    async componentWillMount() {
        let data =  awaitthis.loadData();

        console.log(data);

        this.setState({ products: data });
    }

    loadData = async () => {
        let shopId = this.props.match.params.shopId;

        return await axios.get(`/api/v1/product?shopId=${shopId}`)
            .then(({ data }) => data);
    }

    deleteProduct = async (event, id) => {
        event.stopPropagation();
        let result = await axios.delete(`/api/v1/product/${id}`);
        if (result === 200) {
            let data = await this.loadData();
            this.setState({ products: data });
        }
    }

    render() {

        const { products } = this.state
        let productsData;

        productsData = products && products.map((value, index) => {
            return (
                <div key={value.id}>
                    <h2>{index + 1}. Продукт {value.id}</h2>
                    <div>
                        <span>Название: {value.name}</span>
                    </div>
                    <div>
                        <span>Цена: {value.price} руб.</span>
                    </div>
                    <div>
                        <span>Описание: {value.description}</span>
                    </div>

                    <ControlPanel>
                        <Block><Link to={`/shops/products/edit/${value.id}`}>Редактировать</Link></Block>
                        <Block><a href="http://удалименядруг.рф" onClick={(event) => this.deleteProduct(event, value.id)}>Удалить</a></Block>
                    </ControlPanel>
                </div>
            )
        })

        return (
            <Container>
                <span>Просмотр товаров</span>
                <ControlPanel>
                    <Block><Link to="/">На главную</Link></Block>
                    <Block><Link to={`/shops`}>Список магазинов</Link></Block>
                    <Block><Link to={`/shops/products/create/${this.props.match.params.shopId}`}>Добавить товар</Link></Block>
                </ControlPanel>
                {productsData}
            </Container>

        );
    }
}

export default Products;