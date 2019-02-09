import React, { Component } from 'react'
import { Link } from 'react-router-dom'
import axios from 'axios'
import { Redirect } from 'react-router'

import { Container } from './styled'


class NewShop extends Component {

    state = {
        values: {
            id: '',
            name: '',
            address: '',
            phone: ''
        },
        redirect: false
    }

    async componentWillMount() {
        let parId = this.props.match.params.id;

        if (parId) {
            let data = await axios.get(`/api/v1/shop/${parId}`)
                .then(({ data }) => data);

            console.log(data);
            this.setState({ values: data });
        }
    }

    createShop = async event => {
        //TODO: вызвать api
        const { values } = this.state
        console.log(values)

        let result = values.id === ''
            ? await axios.post('/api/v1/shop', values)
            : await axios.put('/api/v1/shop', values);

        if (result.status === 200)
            this.setState({ redirect: true });
    }

    changeField = event => {
        const target = event.target

        this.setState(function (prevState) {
            return {
                values: { ...prevState.values, [target.name]: target.value },
            }
        })
    }

    render() {
        const { redirect, values } = this.state
        return (
            <Container>
                {redirect ? <Redirect to="/shops" /> : ''}
                <div>
                    <div>Название</div>
                    <div><input name="name" value={values.name} onChange={this.changeField} /></div>
                </div>

                <div>
                    <div>Адрес</div>
                    <div><input name="address" value={values.address} onChange={this.changeField} /></div>
                </div>

                <div>
                    <div>Телефон</div>
                    <div><input name="phone" value={values.phone} onChange={this.changeField} /></div>
                </div>

                <div>
                    <button onClick={this.createShop}>Сохранить</button>
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

export default NewShop;