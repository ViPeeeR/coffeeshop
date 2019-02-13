import React, { Component } from 'react'
import axios from 'axios'
import { withRouter } from 'react-router'

import Item from './Item'

import { Container } from './styled'
import { apiLoadProducts, apiDeleteProduct } from '../../../../../api';

class ProductList extends Component {

    state = {
        products: null,
        selected: 0,
        amount: 0
    }

    async componentWillMount() {
        let data = await this.loadData();

        console.log(data);

        this.setState({ products: data });
    }

    loadData = async () => {
        let shopId = this.props.match.params.shopId || this.props.shop;

        return await apiLoadProducts(shopId);
    }

    deleteProduct = async (event, id) => {
        event.preventDefault();

        await apiDeleteProduct(id);
        let data = await this.loadData();
        this.setState({ products: data });
    }

    onSelectProduct = (value, act) => {
        this.props.onAddingProduct(act, value.price, value.id);
        this.setState((prevState) => {
            return {
                selected: prevState.selected + act,
                amount: prevState.amount + (act > 0 ? value.price : -value.price)
            }
        })
    }

    render() {

        const { products } = this.state
        const { admin, client } = this.props

        let productsData = products && products.map((value, index) => {
            return (
                <Item key={value.id} value={value} index={index + 1} onRemove={this.deleteProduct} admin={admin} client={client} onSelect={this.onSelectProduct} />
            )
        })

        return (
            <Container>
                {productsData}
            </Container>

        );
    }
}

export default withRouter(ProductList);