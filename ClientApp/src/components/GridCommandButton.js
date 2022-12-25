import CheckIcon from '@mui/icons-material/Check';
import CloseIcon from '@mui/icons-material/Close';
import { Button, IconButton } from '@mui/material';

export default function GridCommandButton(props) {
    const icons = {
        commit: <CheckIcon />,
        cancel: <CloseIcon />,
        add: null,
        edit: null,
        delete: null,
    }

    return (props.id === 'commit' || props.id === 'cancel')
        ? (<IconButton color="primary" id={props.id} onClick={props.onExecute}>{icons[props.id]}</IconButton>)
        : (<Button id={props.id} onClick={props.onExecute}>{props.text}</Button>);
}