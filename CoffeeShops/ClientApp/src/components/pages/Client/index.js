import React, { Component } from 'react'
import axios from 'axios'
import { Link } from 'react-router-dom'
import { EditContainer } from './styled';


class Client extends Component {

    state = {
        clients: null
    }

    async componentWillMount() {
        let data = await axios.get('/api/v1/client')
            .then(({ data }) => data);

        console.log(data);

        this.setState({ clients: data });
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