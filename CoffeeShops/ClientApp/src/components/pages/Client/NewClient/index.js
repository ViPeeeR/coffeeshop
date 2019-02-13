import React, { Component } from 'react'
import { Link } from 'react-router-dom'
import { Redirect } from 'react-router'
import axios from 'axios'

import { Container } from './styled'
import { apiLoadClient, apiCreateClient, apiUpdateClient } from '../../../../api';

class NewClient extends Component {

    state = {
        values: {
            id: '',
            firstName: '',
            lastName: '',
            middleName: '',
            birthday: '',
            phone: '',
            sex: 'Male'
        },
        redirect: false,
    }

    async componentWillMount() {
        let parId = this.props.match.params.id;

        if (parId) {
            let data = await apiLoadClient(parId);

            console.log(data);
            data.birthday = data.birthday && data.birthday !== '' ?
                data.birthday.substr(0, data.birthday.indexOf('T')) : '';
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

    createClient = async event => {

        //TODO: вызвать api
        const { values } = this.state
        console.log(values)

        if (!values.firstName || !values.lastName || !values.middleName || !values.phone || !values.birthday) {
            alert('Заполните все обязательные поля!');
            return;
        }

        let result = values.id === ''
            ? await apiCreateClient(values)
            : await apiUpdateClient(values);

        if (result.status === 200)
            this.setState({ redirect: true });
    }

    render() {

        const { redirect, values } = this.state;

        return (
            <Container>
                {redirect ? <Redirect to="/clients" /> : ''}

                <div>
                    <div>Фамилия</div>
                    <div><input name="lastName" value={values.lastName || ''} onChange={this.changeField} /></div>
                </div>

                <div>
                    <div>Имя</div>
                    <div><input name="firstName" value={values.firstName} onChange={this.changeField} /></div>
                </div>

                <div>
                    <div>Отчество</div>
                    <div><input name="middleName" value={values.middleName || ''} onChange={this.changeField} /></div>
                </div>

                <div>
                    <div>Дата рождения</div>
                    <div><input name="birthday" value={values.birthday} type="date" onChange={this.changeField} /></div>
                </div>

                <div>
                    <div>Телефон</div>
                    <div><input name="phone" value={values.phone} onChange={this.changeField} /></div>
                </div>

                <div>
                    <div>Пол</div>
                    <div><select name="sex" value={values.sex === 0 || values.sex === 'Male' ? 'Male' : 'Female'} onChange={this.changeField}>
                        <option>Male</option>
                        <option>Female</option>
                    </select></div>
                </div>

                <div>
                    <button onClick={this.createClient}>Сохранить</button>
                </div>

                <div>
                    <div>
                        <Link to="/clients">К списку клиентов</Link>
                    </div>
                    <div>
                        <Link to="/">На главную</Link>
                    </div>
                </div>
            </Container>
        );
    }
}

export default NewClient;