import React, { Component } from 'react'
import { Link } from 'react-router-dom'

import { Container, ControlPanel, Block, Counter } from './styled'

class Item extends Component {

    state = {
        count: 0
    }

    deleteProduct = async (event, id) => {
        event.preventDefault();

        this.props.onRemove(event, id);
    }

    increase = () => {
        this.setState((prevstate) => {
            return {
                count: prevstate.count + 1
            }
        })
    }

    decrease = () => {
        this.setState((prevstate) => {
            return {
                count: prevstate.count == 0 ? 0 : prevstate.count - 1
            }
        })
    }

    render() {
        const { count } = this.state

        const { admin, client, value, index } = this.props

        return (
            <Container>
                <h2>{index}. Продукт {value.id}</h2>
                <div>
                    <span>Название: {value.name}</span>
                </div>
                <div>
                    <span>Цена: {value.price} руб.</span>
                </div>
                <div>
                    <span>Описание: {value.description}</span>
                </div>
                {
                    admin ?
                        <ControlPanel>
                            <Block><Link to={`/shops/products/edit/${value.id}`}>Редактировать</Link></Block>
                            <Block><a href="http://уменятакдругумер.рф" onClick={(event) => this.deleteProduct(event, value.id)}>Удалить</a></Block>
                        </ControlPanel>
                        : ''
                }

                {
                    client ?
                        <ControlPanel>
                            <Counter><button onClick={this.increase}>+</button>{count}<button onClick={this.decrease}>-</button></Counter>
                        </ControlPanel>
                        : ''
                }
            </Container>
        )
    }
}

export default Item;