import React, { Component } from 'react'
import axios from 'axios'
import { Link } from 'react-router-dom'
import { EditContainer } from './styled';
import { apiLoadClients, apiDeleteClient } from '../../../api';


class Client extends Component {

    state = {
        clients: null
    }

    async componentWillMount() {
        await this.loadData();
    }

    loadData = async () => {
        let data = await apiLoadClients();
        this.setState({ clients: data });
        console.log(data);
    }

    deleteClient = async (event, id) => {
        event.preventDefault();
        await apiDeleteClient(id);
        await this.loadData();
    }

    render() {

        const { clients } = this.state
        let usersData;

        usersData = clients && clients.map((value, index) => {
            return (
                <div key={value.id}>
                    <h2>{index + 1}. Клиент {value.id}</h2>
                    <div>
                        <span>Фамилия: {value.lastName}</span>
                    </div>
                    <div>
                        <span>Имя: {value.firstName}</span>
                    </div>
                    <div>
                        <span>Отчество: {value.middleName}</span>
                    </div>
                    <div>
                        <span>Номер телефона: {value.phone}</span>
                    </div>
                    <div>
                        <span>Дата рождения: {new Date(value.birthday).toLocaleDateString()}</span>
                    </div>

                    <EditContainer><Link to={`/clients/edit/${value.id}`}>Редактировать</Link></EditContainer>
                    <EditContainer><a href="" onClick={(event) => this.deleteClient(event, value.id)}>Удалить</a></EditContainer>
                </div>
            )
        })

        return (
            <div>
                <span>Просмотр клиентов</span>
                <div>
                    <Link to="/">На главную</Link>
                </div>
                {usersData}
            </div>

        );
    }
}

export default Client;