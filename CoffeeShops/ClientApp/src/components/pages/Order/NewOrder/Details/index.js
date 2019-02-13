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
            statusPayment: 'paid'
        }
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
            clientId: '308e0521-6694-4080-9a5c-9e915e8de8f3'
        };

        console.log(sendData);
        await apiCreateOrder(sendData);
    }

    render() {

        const { order } = this.props

        return (
            <Container>

                {
                    !order.shopId ? <Redirect to="/orders/create" /> : ''
                }

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