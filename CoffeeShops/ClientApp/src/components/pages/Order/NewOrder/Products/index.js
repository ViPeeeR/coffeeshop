import React, { Component } from 'react'
import { Redirect } from 'react-router'
import ProductList from '../../../Shop/Products/List'
import { Container } from './styled'

class OrderProducts extends Component {

    state = {
        products: {
            items: [],
            count: 0,
            amount: 0
        },
        toBasket: false
    }

    onAddingProduct = (act, amount, id) => {
        this.setState((prevState) => {
            const { products } = prevState;
            let items = products.items || [];

            if (act < 0) {
                let i = items.findIndex((value) => {
                    return (value === id);
                });
                items.splice(i, 1);
            }
            else {
                items.push(id);
            }

            return {
                products: {
                    items: items,
                    count: (products.count ? products.count : 0) + act,
                    amount: (products.amount ? products.amount : 0) + (act > 0 ? amount : -amount)
                }
            }
        });
    }

    updateBasket = (event, value) => {
        event.preventDefault();

        let dic = {};
        for (var i = 0; i < value.items.length; i++) {
            dic[value.items[i]] = (dic[value.items[i]] || 0) + 1
        }

        let products = [];
        for (var key in dic) {
            products.push({ productId: key, count: dic[key]})
        }

        let basket = { amount: value.amount, products: products }

        this.props.updateBasket(basket)
        this.setState({ toBasket: true })
    }

    render() {

        const { toBasket, products } = this.state;
        const { count, amount } = products;

        const { shop } = this.props;

        return (
            <Container>
                {
                    toBasket ? <Redirect to="/orders/create/details" /> : ''
                }

                <h2>Выберите продукты</h2>

                <div>Выбрано позиций: {count || 0}</div>
                <div>На сумму: {amount || 0}</div>
                {
                    count && count > 0 ?
                        <div><a href="1" onClick={(event) => this.updateBasket(event, products)}>Оформить заказ</a></div>
                        : ''
                }

                <ProductList client onAddingProduct={this.onAddingProduct} shop={shop} />
            </Container>
        )
    }
}

export default OrderProducts;