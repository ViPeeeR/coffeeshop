import React, { Component } from 'react'
import { Link } from 'react-router-dom'

class Home extends Component {

    render() {
        return (
            <div>
                <h2>Клиенты</h2>
                <ul>
                    <li>
                        <Link to="/clients/create">Создать</Link>
                    </li>
                    <li>
                        <Link to="/clients">Просмотр</Link>
                    </li>
                </ul>

                <h2>Магазины</h2>
                <ul>                    
                    <li>
                        <Link to="/shops/create">Создать</Link>
                    </li>
                    <li>
                        <Link to="/shops">Просмотр</Link>
                    </li>
                </ul>

                <h2>Заказы</h2>
                <ul>
                    <li>
                        <Link to="/orders/create">Создать</Link>
                    </li>
                    <li>
                        <Link to="/orders">Просмотр</Link>
                    </li>
                </ul>
            </div>

        );
    }
}

export default Home;