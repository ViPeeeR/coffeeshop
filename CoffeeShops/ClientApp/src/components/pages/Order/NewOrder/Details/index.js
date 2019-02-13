import React, { Component } from 'react'
import { Redirect } from 'react-router'
import axios from 'axios'

import { Container } from './styled'
import { apiCreateOrder } from '../../../../../api';

class Details extends Component {

    state = {
        values: {
            dateDelivery: '',
            comment: '',
            statusPayment: 'paid',
            clientId: ''
        },
        redirect: false
    }

    goPay = () => {
        // TODO: оплатить и послать на сервер.
    }

    changeField = event => {
        const target = event.target

        this.setState(function (prevState) {
            return {
                values: { ...prevState.values, [target.name]: target.value },
            }
        })
    }

    sendOrder = async (event) => {
        // TODO: сгенерировать json и отправить
        const { values } = this.state
        const { order } = this.props        

        let sendData = {
            dateDelivery: values.dateDelivery,
            comment: values.comment,
            statusPayment: values.statusPayment,
            products: order.basket.products,
            shopId: order.shopId,
            clientId: values.clientId
        };

        console.log(sendData);
        await apiCreateOrder(sendData);

        this.setState({ redirect : true})
    }

    render() {

        const { order } = this.props
        const { redirect } = this.state

        return (
            <Container>

                {
                    !order.shopId || redirect ? <Redirect to="/orders/create" /> : ''
                }
                <div>
                    <div>Клиент</div>
                    <div><input name="clientId" onChange={this.changeField} /></div>
                </div>
                <div>
                    <div>Дата доставки</div>
                    <div><input name="dateDelivery" type="datetime-local" onChange={this.changeField} /></div>
                </div>
                <div>
                    <div>Комментарий</div>
                    <div>
                        <textarea name="comment" onChange={this.changeField} />
                    </div>
                </div>
                <div>
                    <input type="radio" name="statusPayment" value="paid" checked={true} onChange={this.changeField}/>Оплатить онлайн
                    <input type="radio" name="statusPayment" value="cash" onChange={this.changeField} />Оплатить при получении
                </div>
                <div>
                    <button onClick={this.sendOrder}>Подтвердить</button>
                </div>
            </Container>
        )
    }
}

export default Details;