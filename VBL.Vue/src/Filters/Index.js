let usDollars = (amount) => amount ? '$' + parseFloat(amount).toFixed(2) : ''
let usPhone = (phone) => phone ? phone.replace(/[^0-9]/g, '').replace(/(\d{3})(\d{3})(\d{4})/, '($1) $2-$3') : ''

export {usDollars, usPhone}
