import React, { Component } from 'react'
import { Link } from 'react-router-dom'

import { Container } from './styled'

class NewClient extends Component {

    createClient = event => {
        //TODO: вызвать api
    }

    render() {
        return (
            <Container>
                <div>
                    <div>Имя</div>
                    <div><input name="firstName" /></div>
                </div>

                <div>
                    <div>Фамилия</div>
                    <div><input name="lastName" /></div>
                </div>

                <div>
                    <div>Отчество</div>
                    <div><input name="middleName" /></div>
                </div>

                <div>
                    <div>Дата рождения</div>
                    <div><input name="birthday" type="date" /></div>
                </div>

                <div>
                    <div>Пол</div>
                    <div><select name="sex" >
                        <option>М</option>
                        <option>Ж</option>
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