import React, { Component } from 'react'
import { Redirect } from 'react-router'

import { Container, Wrapper } from './styled'
import { apiLogin } from '../../../api';


class Login extends Component {

    state = {
        login: '',
        password: ''
    }

    changeField = event => {
        const target = event.target

        this.setState(function (prevState) {
            return {
                values: { ...prevState.values, [target.name]: target.value },
            }
        })
    }

    login = async event => {

        //TODO: вызвать api
        const { values } = this.state
        console.log(values)

        if (!values.login || !values.password) {
            alert('Заполните все обязательные поля!');
            return;
        }

        let result = await apiLogin(values);

        if (result.status === 200)
            this.setState({ redirect: true });
    }

    render() {
        const { redirect } = this.state;

        if (redirect) return <Redirect to="/" />

        return (
            <Container>
                <Wrapper>
                    <div className="form-signin">
                        <h2 className="form-signin-heading">Добро пожаловать!</h2>
                        <input type="text" className="form-control" placeholder="Логин" name="login" onChange={this.changeField} />
                        <input type="password" className="form-control" placeholder="Пароль" name="password" onChange={this.changeField} />
                        <button className="btn btn-lg btn-primary btn-block" onClick={this.login}>Войти</button>
                    </div>
                </Wrapper>
            </Container>
        );
    }
}

export default Login;