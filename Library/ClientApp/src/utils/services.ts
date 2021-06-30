export const handleHttpErrors = (err: any) => {
  if (err.status === 400 || err.status === 500) {
    return err.error;
  } else {
    return "A problem has occurred, please try again.";
  }
};
