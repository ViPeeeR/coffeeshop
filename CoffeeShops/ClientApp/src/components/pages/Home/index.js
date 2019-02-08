import React, { Component } from 'react'
import { Link } from 'react-router-dom'

class Home extends Component {

    render() {
        return (
            <div>
                <ul>
                    <li>
                        <Link to="/create/client">Создать клиента</Link>
                    </li>
                    <li>
                        <Link to="/create/shop">Создать магазин</Link>
                    </li>
                </ul>
                <a></a>
                <a>Добавить товары к магазину</a>
                <a>Выбрать товары - заказать товар</a>
            </div>

        );
    }
}

export default Home;