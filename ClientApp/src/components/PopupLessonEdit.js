import { AsyncSearchSelect } from "./AsyncSearchSelect";

const Popup = ({
    row,
    onChange,
    onApplyChanges,
    onCancelChanges,
    open,
  }) => (
    <Dialog open={open} onClose={onCancelChanges} aria-labelledby="form-dialog-title">
      <DialogTitle id="form-dialog-title">Employee Details</DialogTitle>
      <DialogContent>
        <MuiGrid container spacing={3}>
          <MuiGrid item xs={6}>
            <FormGroup>
              <AsyncSearchSelect
                dataPath='api/teachers'
                optionName='fullName'
                label="Викладач"
              />
            </FormGroup>
          </MuiGrid>
        </MuiGrid>
      </DialogContent>
      <DialogActions>
        <Button onClick={onCancelChanges} color="secondary">
          Cancel
        </Button>
        <Button onClick={onApplyChanges} color="primary">
          Save
        </Button>
      </DialogActions>
    </Dialog>
  );
  