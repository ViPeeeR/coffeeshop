import React, { Component } from 'react'
import axios from 'axios'
import { withRouter } from 'react-router'
import { Link } from 'react-router-dom'

import Item from './Item'

import { Container } from './styled'

class ProductList extends Component {

    state = {
        products: null
    }

    async componentWillMount() {
        let data = await this.loadData();

        console.log(data);

        this.setState({ products: data });
    }

    loadData = async () => {
        let shopId = this.props.match.params.shopId;

        return await axios.get(`/api/v1/product?shopId=${shopId}`)
            .then(({ data }) => data);
    }

    deleteProduct = async (event, id) => {
        event.preventDefault();

        await axios.delete(`/api/v1/product/${id}`);
        let data = await this.loadData();
        this.setState({ products: data });
    }

    increase = () => {
        this.setState((prevstate) => {
            return {
                count: prevstate.count + 1
            }
        })
    }

    decrease = () => {
        this.setState((prevstate) => {
            return {
                count: prevstate.count - 1
            }
        })
    }

    render() {

        const { products } = this.state
        const { admin, client } = this.props

        let productsData = products && products.map((value, index) => {
            return (
                <Item key={value.id} value={value} index={index + 1} onRemove={this.deleteProduct} admin={admin} client={client} />
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