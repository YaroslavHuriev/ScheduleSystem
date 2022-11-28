import React, { useEffect, useState } from 'react';
import Box from '@mui/material/Box';
import axios from "axios";
import { EditingState } from '@devexpress/dx-react-grid';
import CircularProgress from '@mui/material/CircularProgress';
import { useNavigate } from "react-router-dom";
import {
    PagingState,
    IntegratedPaging,
} from '@devexpress/dx-react-grid';
import {
    Grid,
    Table,
    TableHeaderRow,
    TableEditRow,
    TableEditColumn,
    PagingPanel,
} from '@devexpress/dx-react-grid-material-ui';
import { SuccessSnackBar } from './SuccessSnackBar';

const editColumnMessages = {
    addCommand: 'Додати',
    editCommand: 'Редагувати',
    deleteCommand: 'Видалити',
    commitCommand: 'Зберегти',
    cancelCommand: 'Відмінити'
};

const columns = [
    { name: 'name', title: 'Назва' }
];

const getRowId = row => row.id;

export function GroupsList(props) {
    const [rows, setRows] = useState([]);
    const [loading, setLoading] = useState(true);
    const [snackbarState, setSnackbarState] = useState({
        open: false,
        message: ''
    });

    const navigate = useNavigate();
    useEffect(() => {
        axios.get('api/groups').then((response) => {
            setRows(response.data);
            setLoading(false)
        })
    }, [])

    const handleSnackbarClose = (event, reason) => {
        setSnackbarState({
            ...snackbarState,
            open: false
        });
    };

    const commitChanges = ({ added, changed, deleted }) => {
        let changedRows;
        if (added) {
            axios.post('api/groups', { name: added[0].name }).then((response) => {
                setSnackbarState({ open: true, message: 'Група успішно додана!' })
                console.log(response.data)
                const startingAddedId = response.data;
                changedRows = [
                    ...rows,
                    ...[{
                        id: startingAddedId,
                        ...added[0],
                    }],
                ];
                setRows(changedRows);
            })

        }
        if (changed) {
            changedRows = rows.map(row => (changed[row.id] ? { ...row, ...changed[row.id] } : row));
        }
        if (deleted) {
            axios.delete(`api/groups/${deleted[0]}`).then((response) => {
                setSnackbarState({ open: true, message: 'Група успішно видалена!' })
                const deletedSet = new Set(deleted);
                changedRows = rows.filter(row => !deletedSet.has(row.id));
                setRows(changedRows);
            })
        }
    };



    let contents = loading
        ? <Box sx={{ display: 'flex' }}>
            <CircularProgress />
        </Box>
        : <Box sx={{ height: 400, width: '100%', mt: 4 }}>
            <Grid
                rows={rows}
                columns={columns}
                getRowId={getRowId}
            >
                <PagingState
                    defaultCurrentPage={0}
                    pageSize={5}
                />
                <IntegratedPaging />
                <EditingState
                    onCommitChanges={commitChanges}
                />
                <Table />
                <TableHeaderRow />
                <TableEditRow />
                <TableEditColumn
                    showAddCommand
                    showDeleteCommand
                    messages={editColumnMessages}
                />
                <PagingPanel />
            </Grid>
            <SuccessSnackBar
                open={snackbarState.open}
                message={snackbarState.message}
                handleClose={handleSnackbarClose}
            ></SuccessSnackBar>
        </Box>;

    return (
        <div>
            {contents}
        </div>
    );
}
