import styled from 'styled-components'

export const Container = styled.div``

export const ControlPanel = styled.div`
  margin-top: 10px;
  & > span {
    padding-right: 10px;
  }
`

export const Block = styled.span``

export const Counter = styled.div`
    button {
        margin: 0 10px;
        width: 25px;
        height: 25px;

        &:first-child {
            margin-left: 0;
        }

        &:last-child {
            margin-right: 0;
        }
    }
`