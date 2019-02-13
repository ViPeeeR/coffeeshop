import React, { Component } from 'react'
import { Link } from 'react-router-dom'
import { Redirect } from 'react-router'
import axios from 'axios'
import { Container } from './styled'
import { apiLoadProduct, apiCreateProduct, apiUpdateProduct } from '../../../../../api';


class NewProducts extends Component {

    state = {
        values: {
            id: '',
            name: '',
            price: '',
            description: '',
            shopId: ''
        },
        redirect: false,
    }

    async componentWillMount() {
        let id = this.props.match.params.id;

        if (id) {
            let data = await apiLoadProduct(id);
            console.log(data);
            this.setState({ values: data });
        }
    }

    changeField = event => {
        const target = event.target

        this.setState(function (prevState) {
            return {
                values: { ...prevState.values, [target.name]: target.value },
            }
        })
    }

    createProduct = async event => {
        //TODO: вызвать api
        const { values } = this.state

        if (!values.name || !values.price || !values.description || values.price <= 0) {
            alert('Заполните все обязательные поля!');
            return;
        }

        let shopId = this.props.match.params.shopId;
        values.shopId = !shopId ? values.shopId : shopId;

        console.log(values)

        let result = values.id === ''
            ? await apiCreateProduct(values)
            : await apiUpdateProduct(values);

        if (result.status === 200)
            this.setState({ redirect: true });
    }

    render() {
        const { redirect, values } = this.state


        return (
            <Container>
                {redirect ? <Redirect to={`/shops/products/list/${values.shopId}`} /> : ''}

                <div>
                    <div>Название</div>
                    <div><input name="name" value={values.name} onChange={this.changeField} /></div>
                </div>

                <div>
                    <div>Цена</div>
                    <div><input name="price" type="number" value={values.price} onChange={this.changeField} /></div>
                </div>

                <div>
                    <div>Описание</div>
                    <div><textarea name="description" value={values.description} onChange={this.changeField} /></div>
                </div>

                <div>
                    <button onClick={this.createProduct}>Сохранить</button>
                </div>

                <div>
                    <div>
                        <Link to="/shops">К списку магазинов</Link>
                    </div>
                    <div>
                        <Link to="/">На главную</Link>
                    </div>
                </div>
            </Container>
        );
    }
}

export default NewProducts;